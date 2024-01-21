import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  errorOccurs = signal(false);
  successOccurs = signal(false);

  errorMsg = signal('');
  successMsg = signal('');

  triggerErrorNotification(msg: string) {
    this.errorOccurs.set(true);
    this.errorMsg.set(msg);
    setTimeout(() => {
      this.errorOccurs.set(false);
    }, 2000);
  }

  triggerSuccessNotification(msg: string) {
    this.successOccurs.set(true);
    this.successMsg.set(msg);
    setTimeout(() => {
      this.successOccurs.set(false);
    }, 2000);
  }
}
