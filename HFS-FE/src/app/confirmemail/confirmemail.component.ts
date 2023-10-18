import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss']
})
export class ConfirmemailComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userId = params['userId'];
      const code = params['code'];

      // Sử dụng userId và code theo nhu cầu của bạn
      console.log('userId:', userId);
      console.log('code:', code);


  })

}
}
