namespace ParserCombinators.Model

// filter expression is a simple AST (abstract syntax tree) representing our filter
type NewsFilterExpression =
    | Condition of NewsCondition
    | And of NewsFilterExpression * NewsFilterExpression
    | Or of NewsFilterExpression * NewsFilterExpression
    
    // C# style to for smooth interop
    member this.ShouldBeVisibleTo user =
        let rec evaluate user expression =
            match expression with
            | Condition condition -> NewsCondition.evaluate user condition
            | And (left, right) -> if evaluate user left then evaluate user right else false
            | Or (left, right) -> if evaluate user left then true else evaluate user right
        
        evaluate user this