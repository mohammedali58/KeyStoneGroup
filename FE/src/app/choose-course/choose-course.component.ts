import { Component, computed, inject } from '@angular/core';
import { AssetsService } from '../services/assets.service';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Course } from '../Types/course.type';
import { DatePipe } from '@angular/common';
import { NotificationService } from '../services/notifications.service';

@Component({
  selector: 'app-choose-course',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './choose-course.component.html',
  styleUrl: './choose-course.component.css',
})
export class ChooseCourseComponent {
  assets = inject(AssetsService);
  notificationService = inject(NotificationService);

  loadingCourses = false;

  personalDetails = '';

  chosenCourse = computed(() => this.assets.course());
  languages = ['English', 'Swedish', 'Not Specified'];
  deliveryMethods = ['Klassrum', 'Distans', 'Distans med träffar'];
  courses: Course[] = [];
  pageCourses: Course[] = [];
  startIndex = 0;
  previousDisabled = true;
  nextDisable = false;

  ngOnInit() {
    const exitingCourse = localStorage.getItem('courseName');
    if (!exitingCourse) {
      this.assets.getCourses('', '', 0, 0).subscribe({
        next: (result) => {
          this.courses = result;
          this.pageCourses = this.courses.slice(
            this.startIndex,
            this.startIndex + 6
          );
          this.startIndex += 6;
          this.loadingCourses = false;
        },
      });
    } else {
      this.assets.getCourses(exitingCourse, '', 0, 0).subscribe({
        next: (result) => {
          this.assets.course.set(result[0]);
          this.loadingCourses = false;
        },
      });
    }
  }

  searchForm = new FormGroup({
    courseName: new FormControl(''),
    category: new FormControl(''),
    languageId: new FormControl(''),
    deliveryMethodId: new FormControl(''),
  });

  onSubmit(event: Event) {
    event.preventDefault();
    this.loadingCourses = true;
    const { courseName, category, languageId, deliveryMethodId } =
      this.searchForm.value;
    this.assets
      .getCourses(
        courseName ?? '',
        category ?? '',
        Number(languageId) ?? 0,
        Number(deliveryMethodId) ?? 0
      )
      .subscribe({
        next: (result) => {
          this.courses = result;
          this.startIndex = 0;
          this.pageCourses = this.courses.slice(
            this.startIndex,
            this.startIndex + 6
          );
          this.previousDisabled = true;
          this.nextDisable = false;
          if (this.courses.length > 6) {
            this.startIndex += 6;
            this.previousDisabled = true;
          }
          if (this.courses.length <= 6) {
            this.nextDisable = true;
          }
          this.loadingCourses = false;
          this.loadingCourses = false;
        },
      });
  }

  onChoseCourse(id: string) {
    const course = this.courses.filter((c) => c.id == id)[0];
    this.assets.course.set(course);
    this.courses = [];
    localStorage.setItem('courseId', course.id);
    localStorage.setItem('courseName', course.courseName);
    this.notificationService.triggerSuccessNotification(
      'Done! please sent your contact info.'
    );
  }

  onREmoveCourse() {
    this.assets.course.set(null);
    localStorage.removeItem('courseName');
    localStorage.removeItem('courseId');
    this.assets.getCourses('', '', 0, 0).subscribe({
      next: (result) => {
        this.courses = result;
        this.loadingCourses = false;
      },
    });
  }

  onNextPage() {
    this.previousDisabled = false;
    this.pageCourses = this.courses.slice(this.startIndex, this.startIndex + 6);
    this.startIndex += 6;

    if (this.startIndex >= this.courses.length) {
      this.nextDisable = true;
    }
  }

  onPreviousPage() {
    this.nextDisable = false;
    console.log(this.startIndex);

    this.pageCourses = this.courses.slice(
      this.startIndex - 12,
      this.startIndex - 6
    );
    this.startIndex -= 6;

    if (this.startIndex <= 6) {
      this.previousDisabled = true;
    }
  }
}