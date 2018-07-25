# Masterdetail

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 1.7.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

# Usage
This section outlines some of the samples that we have created...

## Input Bindings, Child Component as a Table Row
To see a sample of a master detail component that 
uses input bindings, navigate to /farms and review the 
table that is created.  The parent component contains an 
array of farms and for each farm in the array, the parent 
component creates a child component that is a table row.
Also notice that the child component uses an attribute selector
so that it can be included in a tr element.

These components can be found in [/src/app/farms](src/app/farms/.)

## Parent listens for child event
We have also demonstrated in the farms master detail page that the parent 
componet subscribes to an event that is emitted from each item in the 
child component.  Basically, when the user clicks delete for a given farm,
the child component will emit an event so that the parent component can 
subscribe and delete the item from the table.

This sample is related to the [angular sample](https://angular.io/guide/component-interaction#parent-listens-for-child-event)

These components can be found in [/src/app/farms](src/app/farms/.)
