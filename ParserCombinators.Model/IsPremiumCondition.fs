namespace ParserCombinators.Model

type IsPremiumCondition =
    | Is of bool

// To parse this condition, we need three building blocks:

// First, we need a keyword specifying that we are filtering users by premium status

// Second, we need an operator 'is'.
// Since there is only one operator, don't have to use it,
// but it will allow us to add more operators later

// Third, we need to parse a boolean value

module IsPremiumCondition =
    let evaluate isPremium expression =
        let (Is premiumCondition) = expression
        premiumCondition = isPremium