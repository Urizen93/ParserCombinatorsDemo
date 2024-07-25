namespace ParserCombinators.Model

open FParsec
open FParsec.Pipes
open Helpers

module RoleConditionParser =
    // could be replaced by $["role1", "role2", ...]; made it this way to introduce parsing of literals
    let private allowed = Set.ofList ["Admin"; "Customer"; "Manager"]
    let private roleKeyword = %ci "ROLE"
    
    // for our language, literal is defined as "one or more letters enclosed by single quotes"
    let private literal =
        between %''' %''' <| many1Chars letter
    
    let private singularOperator = %[
        %'=' >>% Equals
        %"!=" >>% NotEquals
    ]
    
    let private pluralOperator = %[
        %ci "IN" >>% In
        %ci "NOT IN" >>% NotIn
    ]
    
    let private singularRole =
        literal
        |>> fun value -> if allowed |> Set.contains value then Some value else None
        >>= fun maybeRoleName -> preturn <!> maybeRoleName <|>% fail "Must be valid role name!"
        |> attempt
    
    let private multipleRoles =
        // sepBy1 parser one or more occurrences of 'singularRole', separated by ','
        between %'(' %')' <| sepBy1 singularRole %','
        |>> Set.ofList
    
    let private singularCondition =
        %% spaces -- +. singularOperator ?- spaces -- +. singularRole -|> id
        // ?- backtracks the whole pipe if anything to the left of it fails.
        // ?- says "if we've gotten this far, we're definitely looking at something
        // that is supposed to match this parser, and if it doesn't, that's a syntax error"
    
    let private pluralCondition =
        %% spaces1 -- +. pluralOperator ?- spaces1 -- +. multipleRoles -|> id
    
    let roleCondition : Parser<RoleCondition, unit> =
        // >>. is a simple way to combine parsers; used when pipe syntax only complicates things
        // <|> is an alternative operator: it tries to apply parser to it's left hand,
        // and applies a right hand parser if it fails
        roleKeyword >>. (singularCondition <|> pluralCondition)