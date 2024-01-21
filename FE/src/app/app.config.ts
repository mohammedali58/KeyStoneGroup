import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { NotificationService } from './services/notifications.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    { provide: NotificationService },
    provideHttpClient(),
  ],
};
