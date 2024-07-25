namespace ParserCombinators.Model

open System.Diagnostics.CodeAnalysis

open FParsec
open FParsec.Pipes
open Helpers

module NewsFilterExpressionParser =
    // F# does not allow referencing undefined members,
    // hence for recursive definitions we have to use this reference trick
    let (private newsFilterCondition : Parser<NewsFilterExpression, unit>), private expressionRef =
        createParserForwardedToRef()

    let private newsCondition = %[
        IsPremiumConditionParser.isPremiumCondition |>> IsPremium
        AgeConditionParser.ageCondition |>> Age
        RoleConditionParser.roleCondition |>> Role
    ]
    
    let private element =
        // Here we say that a node is either a single condition or
        // one or more conditions enclosed in braces
        newsCondition |>> Condition <|> between %'(' %')' newsFilterCondition
    
    // 'curry' splits function of "multiple" arguments into a chain of single-arg functions
    let private ``and`` =
        %% spaces1 -- %ci "and" ?- spaces1 -|> (curry And)
    
    let private ``or`` =
        %% spaces1 -- %ci "or" ?- spaces1 -|> (curry Or)
    
    // 'chainl1' here is a way to chain left-associative operators
    let private firstPriority = chainl1 element ``and``
    let private secondPriority = chainl1 firstPriority ``or``
    
    expressionRef.Value <- secondPriority
    
    let newsFilterParser = newsFilterCondition .>> spaces .>> eof

    let parse input =
        // run parser on input and match its results
        match runParserOnString newsFilterParser () "" input with
        // Choice type is an F#'s way to say "Either one or another value"
        | Success (result, _, _) -> Choice1Of2 result
        | Failure (error, _, _) -> Choice2Of2 error
    
    // C#'s conventional syntax for interop
    let TryParse
            input
            ([<MaybeNull; NotNullWhen true>] rule : NewsFilterExpression outref)
            ([<MaybeNull; NotNullWhen false>] error : string outref) =
        match parse input with
        | Choice1Of2 success ->
            rule <- success
            true
        | Choice2Of2 failure ->
            error <- failure
            false