describe('Do not create todo', () => {
    it('should not create a todo with missing name', () => {
        const todoDate = '2024-08-12';

        // Attempt to create a new todo item with missing name
        cy.visit('/');
        cy.get('#add-date').type(todoDate);
        cy.get('#add').click();

        // Verify the todo item is not added
        cy.get(todoDate).should('not.exist');
    });
});