describe('Delete Todo', () => {
    const todoContent = 'New Testpyramid Systemtest Todo';
    const todoDate = '2024-08-12';

    beforeEach(() => {
        // Create a new todo item before each test
        cy.visit('/');
        cy.get('#add-name').type(todoContent);
        cy.get('#add-date').type(todoDate);
        cy.get('#add').click();
        cy.get('td').contains(todoContent).should('be.visible');
    });

    it('should delete a todo item', () => {
        // Basic Flow
        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Delete').click();
        cy.get(todoContent).should('not.exist');
    });

    it('should not delete a todo item if user is not authenticated', () => {
        // Simulate user not authenticated
        cy.intercept('DELETE', '/api/todo/*', (req) => {
            req.reply((res) => {
                res.send({
                    statusCode: 401,
                    body: { error: 'You must be authenticated to perform this action' },
                });
            });
        }).as('deleteTodo');

        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Delete').click();
        // Wait for the intercepted request and assert its response
        cy.wait('@deleteTodo').its('response').then((response) => {
            expect(response.statusCode).to.eq(401);
            expect(response.body.error).to.eq('You must be authenticated to perform this action');
        });
        cy.get('td').contains(todoContent).should('be.visible');
    });

    it('should not delete a todo item if user is not authorized', () => {
        // Simulate user not authorized
        cy.intercept('DELETE', '/api/todo/*', (req) => {
            req.reply((res) => {
                res.send({
                    statusCode: 403,
                    body: { error: 'You must be authorized to perform this action' },
                });
            });
        }).as('deleteTodo');

        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Delete').click();
        cy.get('td').contains(todoContent).should('be.visible');
        cy.wait('@deleteTodo').its('response').then((response) => {
            expect(response.statusCode).to.eq(403);
            expect(response.body.error).to.eq('You must be authorized to perform this action');
        });
        cy.get('td').contains(todoContent).should('be.visible');
    });

    it('should show an error if the todo item is not found', () => {
        // Simulate Todo item not found
        cy.intercept('DELETE', '/api/todo/*', (req) => {
            req.reply((res) => {
                res.send({
                    statusCode: 404,
                    body: { error: 'Todo item not found' },
                });
            });
        }).as('deleteTodo');

        // Simulate todo item not found
        cy.visit('/');
        cy.get('td').contains(todoContent).parent().find('button').contains('Delete').click();
        cy.wait('@deleteTodo').its('response').then((response) => {
            expect(response.statusCode).to.eq(404);
            expect(response.body.error).to.eq('Todo item not found');
        });
    });
});