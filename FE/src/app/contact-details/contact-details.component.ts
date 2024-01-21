import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AssetsService } from '../services/assets.service';
import { Contact } from '../Types/contact.type';
import { NotificationService } from '../services/notifications.service';

@Component({
  selector: 'app-contact-details',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css',
})
export class ContactDetailsComponent {
  assets = inject(AssetsService);
  notificationService = inject(NotificationService);

  emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  contactFormGroup = new FormGroup({
    fullName: new FormControl(''),
    email: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl(''),
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const { fullName, email, phoneNumber, address } =
      this.contactFormGroup.value;

    if (!fullName || !email || !phoneNumber || !address) {
      this.notificationService.triggerErrorNotification(
        'please complete your Info!'
      );
      return;
    }

    if (!email.match(this.emailRegex)) {
      this.notificationService.triggerErrorNotification('Invalid email');
      return;
    }

    this.assets.createContact(fullName, email, phoneNumber, address).subscribe({
      next: (result: number) => {
        localStorage.setItem('id', result.toString());
        this.assets.getContact(result.toString()).subscribe({
          next: (result) => {
            this.assets.contact.set(result);
            this.assets.onChoseCourse(
              localStorage.getItem('id'),
              localStorage.getItem('courseId')
            );
            this.notificationService.triggerSuccessNotification(
              'Your data was sent to the institution.'
            );
          },
        });
      },
    });
    this.contactFormGroup.reset();
  }

  ngOnInit() {
    this.assets.getContact(localStorage.getItem('id')).subscribe({
      next: (result) => {
        this.assets.contact.set(result);
      },
      error: (er) => console.log(er),
    });
  }

  onClearPersonalData() {
    this.assets.clearPersonalData(localStorage.getItem('id')).subscribe({
      next: () => {
        this.assets.contact.set(null);
        localStorage.removeItem('id');
        this.notificationService.triggerSuccessNotification(
          'Your data cleared successfully'
        );
      },
    });
  }
}
