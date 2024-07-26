namespace ParserCombinators.Model

// This is a discriminated union,
// meaning it's instance could be only one of the listed values

// In more imperative languages, you could represent it by inheritance,
// preferably with closed hierarchy
type AgeCondition =
    | Greater of int
    | GreaterOrEqual of int
    | Less of int
    | LessOrEqual of int

module AgeCondition =
    let evaluate age expression =
        match expression with
        | Greater than -> age > than
        | GreaterOrEqual than -> age >= than
        | Less than -> age < than
        | LessOrEqual than -> age <= than