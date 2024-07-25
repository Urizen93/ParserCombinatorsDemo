namespace ParserCombinators.Model

type RoleCondition =
    | Equals of string
    | NotEquals of string
    | In of string Set
    | NotIn of string Set

module RoleCondition =
    let evaluate userRoles expression =
        let userRolesContain role = Set.contains role userRoles
        let userRolesOverlapWith roles = Set.intersect roles userRoles |> (not << Set.isEmpty)
        
        match expression with
        | Equals role -> userRolesContain role
        | NotEquals role -> not <| userRolesContain role
        | In roles -> userRolesOverlapWith roles
        | NotIn roles -> not <| userRolesOverlapWith roles