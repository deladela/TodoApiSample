import { Given, When, Then } from '@badeball/cypress-cucumber-preprocessor';

const todoContent = 'Old Todo Content';
const initialDate = '2024-08-12';

Given('I have a todo item with content {string}', (content) => {
    cy.visit('/');
    cy.get('#add-name').type(content);
    cy.get('#add-date').type(initialDate);
    cy.get('#add').click();
    cy.get('td').contains(content).should('be.visible');
});

Given('I have a todo item with content {string} and due date {string}', (content, date) => {
    cy.visit('/');
    cy.get('#add-name').type(content);
    cy.get('#add-date').type(date);
    cy.get('#add').click();
    cy.get('td').contains(content).should('be.visible');
});

Given('I have a todo item with content {string} which should result in an empty text', (content) => {
    cy.intercept('PUT', '/api/todo/*').as('updateTodo');
    cy.visit('/');
    cy.get('#add-name').type(content);
    cy.get('#add-date').type(initialDate);
    cy.get('#add').click();
    cy.get('td').contains(content).should('be.visible');
});

When('I update the content to {string}', (newContent) => {
    cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
    cy.get('#edit-name').clear().type(newContent);
    cy.get('#save').click();
});

When('I clear the content', () => {
    cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
    cy.get('#edit-name').clear();
    cy.get('#save').click();
});

When('I update the due date to {string}', (newDate) => {
    cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
    cy.get('#edit-date').clear().type(newDate);
    cy.get('#save').click();
});

Then('the content should be updated to {string}', (newContent) => {
    cy.get('td').contains(newContent).should('be.visible');
});

Then('the due date should be updated to {string}', (newDate) => {
    cy.get('td').contains(todoContent).parent().find('td').contains(newDate).should('be.visible');
});

Then('the request to add an item should fail', () => {
    cy.wait('@updateTodo').its('response').then((response) => {
        expect(response.statusCode).to.eq(400);
    });
});