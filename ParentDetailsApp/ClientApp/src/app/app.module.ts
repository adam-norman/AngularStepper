import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { StepperComponent } from './Components/stepper/stepper.component';
import { ItemsOfStepPipe } from './Pipes/ItemsOfStep.pipe';
import { AlertifyService } from './Services/alertify.service.ts.service';




@NgModule({
  declarations: [
    AppComponent, StepperComponent, ItemsOfStepPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: StepperComponent, pathMatch: 'full' }
    ])
  ],
  providers: [AlertifyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
