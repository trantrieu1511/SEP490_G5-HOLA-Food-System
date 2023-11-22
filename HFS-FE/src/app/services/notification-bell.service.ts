import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Notification } from '../modules/shared-module/models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationBellService {

  private isNewNotify = new BehaviorSubject<boolean>(false);

  isNewNotifyHandle = this.isNewNotify.asObservable();

  private readNotify = new BehaviorSubject<Notification>(new Notification());

  readNotifyHandle = this.readNotify.asObservable();

  constructor() { }

  onIsNewNotifyYet(isNew: boolean){
    this.isNewNotify.next(isNew);
  }

  onReadNotify(notifyRead: Notification){
    this.readNotify.next(notifyRead);
  }
}
