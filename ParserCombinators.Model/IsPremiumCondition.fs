namespace ParserCombinators.Model

type IsPremiumCondition =
    | Is of bool

module IsPremiumCondition =
    let evaluate isPremium expression =
        let (Is premiumCondition) = expression
        premiumCondition = isPremium