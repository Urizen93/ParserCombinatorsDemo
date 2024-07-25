namespace ParserCombinators.Model

// Combining all potential conditions in one type
type NewsCondition =
    | Age of AgeCondition
    | Role of RoleCondition
    | IsPremium of IsPremiumCondition

module NewsCondition =
    let evaluate user expression =
        match expression with
        | Age ageCondition -> ageCondition |> AgeCondition.evaluate user.Age
        | Role roleCondition -> roleCondition |> RoleCondition.evaluate user.Roles
        | IsPremium isPremiumCondition -> isPremiumCondition |> IsPremiumCondition.evaluate user.IsPremium