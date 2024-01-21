import { HttpClient } from '@angular/common/http';
import { Injectable, WritableSignal, inject, signal } from '@angular/core';
import { Course } from '../Types/course.type';
import { Contact } from '../Types/contact.type';

@Injectable({
  providedIn: 'root',
})
export class AssetsService {
  contact: WritableSignal<Contact | null> = signal(null);
  course: WritableSignal<Course | null> = signal(null);

  protected _http = inject(HttpClient);

  getCoursesTitles() {
    return this._http.get<string[]>(
      'https://localhost:7002/api/Courses/search'
    );
  }

  getCourses(
    courseName: string | null | undefined,
    category: string | null | undefined,
    languageId: number | null | undefined,
    deliveryMethodId: number | null | undefined
  ) {
    return this._http.get<Course[]>(
      `https://localhost:7002/api/Courses?LanguageId=${languageId}&DeliveryMethodId=${deliveryMethodId}${
        category ? `&Category=${category}` : ''
      }${courseName ? `&courseName=${courseName}` : ''}`
    );
  }

  getContact(id: string | null) {
    return this._http.get<Contact>(
      `https://localhost:7002/api/Contacts?Id=${id}`
    );
  }

  createContact(
    fullName: string | null | undefined,
    email: string | null | undefined,
    phoneNumber: string | null | undefined,
    address: string | null | undefined
  ) {
    return this._http.post<number>('https://localhost:7002/api/Contacts', {
      fullName,
      email,
      phoneNumber,
      address,
    });
  }

  clearPersonalData(id: string | null) {
    this.contact.set(null);
    return this._http.delete<number>(
      `https://localhost:7002/api/Contacts?Id=${id}`
    );
  }

  onChoseCourse(id: string | null, courseId: string | null) {
    if (!id || !courseId) {
      return;
    }

    this._http
      .post<Contact>('https://localhost:7002/api/Courses/choose', {
        contactId: id,
        courseId,
      })
      .subscribe({
        next: (result) => {
          this.contact.set(result);
          this.course.set(result.course);
        },
      });
  }
}
