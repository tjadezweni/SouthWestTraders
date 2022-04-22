<h1 align="center">South West Traders</h1> <br>

## Table Of Contents
- [Project Description](#books-project-description)
- [Project Management](#clipboard-project-management)
- [Git Structure](#gear-git-structure)

## :books: Project Description

South West Traders API, is an api meant to fit the requirement of the fictious company
South West Traders to manage their catalogue, view them, search them, and place orders, 
reserve the, complete, and cancel them.

## :clipboard: Project Management

- [GitHub Project Board](https://github.com/tjadezweni/SouthWestTraders/projects/1?add_cards_query=is%3Aopen)

## :gear: Git Structure

### Code Quality

#### Builds and Testing

[![.NET Core API](https://github.com/tjadezweni/SouthWestTraders/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tjadezweni/SouthWestTraders/actions/workflows/dotnet.yml)

### Branching Strategy

master (build)
</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- develop (build)
</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- feature/featureName 
</br>

### Commit Prefixes

Each commit follows the pattern: (type): (subject)
where:

* type:
   * feat: new feature implementation
   * refactor: changes to "production" code
   * style: changes to code formatting
   * fix: bug fixes
   * test: adding or refactoring of tests
   * docs: adding or changes to documentation
   * chore: updating grunt tasks. No production code changes

* subject: Summary of the task that was done (written in the present tense)