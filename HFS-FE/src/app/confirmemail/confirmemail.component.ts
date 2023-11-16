import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';
@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss']
})
export class ConfirmemailComponent implements OnInit {
  code:string;
  formCofirm:FormGroup;
  showSuccess:number=0;
  constructor(private route: ActivatedRoute
    ,  private service: AuthService) { }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userId = params['userId'];
      const code = params['code'];
      // Sử dụng userId và code theo nhu cầu của bạn
      console.log('userId:', userId);
      console.log('code:', code);
      this.code=code;
      this.formCofirm=new FormGroup({
        confirm: new FormControl(this.code),
        userId: new FormControl("1")
      })
      debugger;
      this.service.confirmemail(this.formCofirm.value).subscribe(res=>{
        this.service.showconfirm$.subscribe(show => {
          this.showSuccess = show;})

      }, error=>{
        console.log(error);

      })

  })

}
}
