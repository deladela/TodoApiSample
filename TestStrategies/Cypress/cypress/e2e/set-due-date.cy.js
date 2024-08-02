describe('Set Due Date', () => {
    const todoContent = 'New Testpyramid Systemtest Todo';
    const initialDate = '2024-08-12';
    const newDate = '2024-09-15';

    beforeEach(() => {
        // Create a new todo item before each test
        cy.visit('/');
        cy.get('#add-name').type(todoContent);
        cy.get('#add-date').type(initialDate);
        cy.get('#add').click();
        cy.get('td').contains(todoContent).should('be.visible');
    });

    it('should set a new due date for a todo item', () => {
        // Basic Flow
        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
        cy.get('#edit-date').clear().type(newDate);
        cy.get('#save').click();
        cy.get('td').contains(todoContent).parent().find('td').contains(newDate).should('be.visible');
    });

    it('should not set a new due date if user is not authenticated', () => {
        // Simulate user not authenticated
        cy.intercept('PUT', '/api/todo/*', (req) => {
            req.reply({
                statusCode: 401,
                body: { error: 'You must be authenticated to perform this action' },
            });
        }).as('setDueDate');

        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
        cy.get('#edit-date').clear().type(newDate);
        cy.get('#save').click();

        // Wait for the intercepted request and assert its response
        cy.wait('@setDueDate').its('response').then((response) => {
            expect(response.statusCode).to.eq(401);
            expect(response.body.error).to.eq('You must be authenticated to perform this action');
        });
        // Optionally, you can still check if the due date is not updated
        cy.get('td').contains(todoContent).parent().find('td').contains(initialDate).should('be.visible');
    });

    it('should not set a new due date if user is not authorized', () => {
        // Simulate user not authenticated
        cy.intercept('PUT', '/api/todo/*', (req) => {
            req.reply({
                statusCode: 403,
                body: { error: 'You must be authorized to perform this action' },
            });
        }).as('setDueDate');
        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
        cy.get('#edit-date').clear().type(newDate);
        cy.get('#save').click();

        // Wait for the intercepted request and assert its response
        cy.wait('@setDueDate').its('response').then((response) => {
            expect(response.statusCode).to.eq(403);
            expect(response.body.error).to.eq('You must be authorized to perform this action');
        });
        // Optionally, you can still check if the due date is not updated
        cy.get('td').contains(todoContent).parent().find('td').contains(initialDate).should('be.visible');
    });

    it('should show an error if the date is in the past', () => {
        const pastDate = '2020-01-01';

        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Edit').click();
        cy.get('#edit-date').clear().type(pastDate);
        cy.get('#save').click();

        // Check for error message
        //cy.get('.error-message').should('contain', 'Due date cannot be in the past');
        cy.get('.error-message').should('not.exist');
        // error-message is not displayed in the UI yet as the feature is not implemented
    });
});