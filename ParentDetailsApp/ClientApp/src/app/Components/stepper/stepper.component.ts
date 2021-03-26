import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/Models/item';
import { Step } from 'src/app/Models/step';
import { AlertifyService } from 'src/app/Services/alertify.service.ts.service';
import { ItemsService } from 'src/app/Services/ItemsService.service';
import { StepsService } from 'src/app/Services/StepsService.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.css'],
})
export class StepperComponent implements OnInit {
  constructor(private stepsService: StepsService, private itemsService: ItemsService, private alertify: AlertifyService) { }
  currentStep: Step;
  currentItem: Item;
  activeStepIndex = 0;
  activeItemIndex = 0;
  addNewStep = false;
  addNewItem = false;
  description = '';
  title = '';
  itemId: number = null;
  steps: Step[] = [];
  items: Item[] = [];


  ngOnInit() {
    this.loadSteps();
  }
  loadSteps() {
    this.stepsService.getAll().subscribe(res => {
      if (res) {
       // tslint:disable-next-line:no-debugger
       debugger;
      this.steps = res ;
      if (this.steps && this.steps.length > 0) {
        this.currentStep = this.steps[0];
        this.loadItems();
      }
      }
    });
  }
  loadItems() {
    this.itemsService.getAll().subscribe(res => {
      if (res) {
      this.items = res ;
      }
    } );
  }
  loadStepItems(step: Step, index: number) {
    // tslint:disable-next-line:no-debugger
    debugger;
    if (step && index >= 0) {
      this.currentStep = step;
      this.activeStepIndex = index;
      this.addNewStep = false;
    } else {
      this.addNewStep = true;
    }
  }
  onItemClick(item: Item, index: number) {
    // tslint:disable-next-line:no-debugger
    debugger;
    if (item && index >= 0) {
      this.currentItem = item;
      this.activeItemIndex = index;
      this.addNewItem = false;
      this.title = item.title;
      this.itemId = item.id;
      this.description = item.description;
    } else {
      this.addNewItem = true;
    }
  }
  AddNewStep() {
    Swal.fire({
      title: 'Confirm',
      text: "Are you sure you need to add new Step",
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Add New Step it!'
    }).then((result) => {
      if (result.isConfirmed) {
         this.stepsService.create({ id: null, title: 'Step'}).subscribe(
      res => {
        console.log(res);
        this.loadSteps();
      }
    );
        Swal.fire(
          'Added!',
          'The Step has been added.',
          'success'
        );
      }
    });

  }
  onSubmit(frm) {

    const addnew: boolean = frm.value.itemId == null ? true : false;
    this.itemsService.create({id: frm.value.itemId, title: frm.value.title, description: frm.value.description, stepId: this.currentStep.id})
    .subscribe(res => {
      this.loadItems();
      addnew ? this.alertify.success('New Item has been added successfuly') : this.alertify.success('The selected item has been updated successfuly');
      frm.resetForm();
    });


  }
  onPrevious(activeStepIndex) {

    this.addNewStep = false;
    if (activeStepIndex >= 0 && activeStepIndex > 0) {
      this.activeStepIndex--;
      this.currentStep = this.steps[this.activeStepIndex];

    }
  }
  onNext(activeStepIndex) {
    this.addNewStep = false;
    if (activeStepIndex >= 0 && this.steps && activeStepIndex < this.steps.length - 1) {
      this.activeStepIndex++;
      this.currentStep = this.steps[this.activeStepIndex];
    }
  }
  deleteStep(step: Step) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.stepsService.delete(step.id).subscribe(res => {
          this.loadSteps();
        });

        Swal.fire(
          'Deleted!',
          'The Step has been deleted.',
          'success'
        );
      }
    });
  }

  deleteItem(item: Item) {
    const stepId = item.stepId;
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.itemsService.delete(item.id).subscribe(res => {
          this.loadItems();
        });

        Swal.fire(
          'Deleted!',
          'The Item has been deleted.',
          'success'
        );
      }
    });
  }


  removeStepById(id) {
    this.steps = this.steps.filter(item => item.id !== id);
  }
  removeItemById(id) {
    this.items = this.items.filter(item => item.id !== id);
  }
}


