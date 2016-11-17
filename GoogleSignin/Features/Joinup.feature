Feature: Joinup
    In order to check my emails
	As a Customer
	I would like to see the sigin page

@UITest
Scenario Outline: Access Sigin form
	Given I am on <Google> website
	When I click on joinup button
	Then i should see the joinup form

Examples: 
| Google            |
| India             |
| Australia         |
