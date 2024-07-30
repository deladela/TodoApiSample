
describe('Create Todo', () => {
    afterEach(() => {
        // Clean up the created todo item
        cy.get('td').contains('New Testpyramid Systemtest Todo').parent().find('button').contains('Delete').click();
    });

    it('should create a new todo', () => {
        const todoContent = 'New Testpyramid Systemtest Todo';
        const todoDate = '2024-08-12';

        cy.visit('/');
        cy.get('#add-name').type(todoContent);
        cy.get('#add-date').type(todoDate);
        cy.get('#add').click();
        cy.get('td').contains(todoContent).should('be.visible');
        cy.get('td').contains(todoDate).should('be.visible');
    });
});