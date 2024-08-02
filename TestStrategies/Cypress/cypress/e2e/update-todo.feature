Feature: Update Todo Item

  Scenario: Update the content of a todo item
    Given I have a todo item with content "Old Todo Content"
    When I update the content to "Updated Todo Content"
    Then the content should be updated to "Updated Todo Content"

  Scenario: Show error if the updated content is empty
    Given I have a todo item with content "Old Todo Content" which should result in an empty text
    When I clear the content
    Then the request to add an item should fail

  Scenario: Update the due date of a todo item
    Given I have a todo item with content "Old Todo Content" and due date "2024-08-12"
    When I update the due date to "2024-09-15"
    Then the due date should be updated to "2024-09-15"