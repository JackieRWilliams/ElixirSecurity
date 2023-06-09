Description:
This is a simple console app to simulate users entering and answering security questions per Elixir guidelines.

Design:
For ease of implementation I chose to use Json files to store both the list of questions and the user responses.  
The questions are all stored in a file named questions.json and are represented by a unique Id 
and question text.  Additional questions can be added very easily by adding a new Json item with a unique Id.

In a real system the user's stored answers would be encrypted and placed in a secured database, but for this 
coding example I believe Json was sufficient.  When a user chooses to store answers a new file entitled 
USERNAME_answers.json is created to store their response.  It is also used to validate their responses when they
answer questions. Each answer is stored with the question unique Id so that the answer can be matched back to the
question and the proper question and answer combination be displayed when executing the Answer flow.

When validating user input I choose to always compare uppercase version of the inputted and stored text for
ease of use.

