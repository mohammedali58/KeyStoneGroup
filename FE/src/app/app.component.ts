import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ContactDetailsComponent } from './contact-details/contact-details.component';
import { ChooseCourseComponent } from './choose-course/choose-course.component';
import { NotificationService } from './services/notifications.service';
import { AssetsService } from './services/assets.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    NavBarComponent,
    RouterOutlet,
    ContactDetailsComponent,
    ChooseCourseComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Keystone';
  notificationService = inject(NotificationService);
  assets = inject(AssetsService);
}
