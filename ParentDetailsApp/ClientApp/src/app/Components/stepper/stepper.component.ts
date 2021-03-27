import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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


  @ViewChild('stepperForm', { static: true }) stepperForm: NgForm;

  currentStep: Step;
  currentItem: Item;
  activeStepIndex = 0;
  activeItemIndex = 0;
  addNewStep = false;
  addUpdateItem = false;

  // Form fields
  description = '';
  title = '';
  id: number = null; // itemId
  stepId: number = null;
  // end
  steps: Step[] = [];
  items: Item[] = [];

  constructor(private stepsService: StepsService, private itemsService: ItemsService, private alertify: AlertifyService) { }
  ngOnInit() {
    this.loadSteps();
  }
  loadSteps() {
    this.stepsService.getAll().subscribe(res => {
      if (res) {
        this.steps = res;
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
        this.items = res;
      }
    });
  }

  loadStepItems(step: Step, index: number) {
    if (step && index >= 0) {
      this.currentStep = step;
      this.activeStepIndex = index;
      this.addNewStep = false;
    } else {
      this.addNewStep = true;
    }
  }

  onItemClick(item: Item, index: number) {
    if (item && index >= 0) {
      this.currentItem = item;
      this.activeItemIndex = index;
      this.addUpdateItem = !this.addUpdateItem;
      this.fillStepperForm(item);
    }
  }

  fillStepperForm(item: Item) {
    this.title = item.title;
    this.id = item.id;
    this.description = item.description;
    this.stepId = item.stepId;
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
        this.stepsService.create({ id: null, title: 'Step' }).subscribe(
          res => {
            this.loadSteps();
          }, (error) => {
            this.alertify.error(error.message);
          }, () => {
            this.currentStep = this.steps[this.steps.length - 1];
            this.activeStepIndex = this.steps.length - 1;
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
     if (!frm.value.stepId) {
      frm.value.stepId = this.currentStep.id;
    }
    const isAddnewOperation: boolean = frm.value.id == null ? true : false;
    this.itemsService.create(frm.value)
      .subscribe(res => {
        this.loadItems();
        isAddnewOperation ? this.alertify.success('New Item has been added successfuly') : this.alertify.success('The selected item has been updated successfuly');
        frm.resetForm();
      }, (error) => {
        this.alertify.error(error.message);
      }, () => {
        this.activeItemIndex = 0;
        this.addUpdateItem = false;
      }
      );
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
        }, (error) => {
          this.alertify.error(error.message);
        }, () => {
          if (this.steps.length > 0) {
            this.currentStep = this.steps[0];
            this.activeStepIndex = 0;
          }
          Swal.fire(
            'Deleted!',
            'The Step has been deleted.',
            'success'
          );
        });
      }
    });
  }

  resetForm() {
    this.addUpdateItem = !this.addUpdateItem;
    this.stepperForm.reset();
    this.stepperForm.value.stepid = this.currentStep.id;
  }

  deleteItem(item: Item) {
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
        }, (error) => {
          this.alertify.error(error.message);
        }, () => {
          if (this.items.length > 0) {
            this.activeItemIndex = 0;
          }
          Swal.fire(
            'Deleted!',
            'The Item has been deleted.',
            'success'
          );
        });
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


