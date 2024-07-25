namespace ParserCombinators.Model

open FParsec
open FParsec.Pipes

module IsPremiumConditionParser =
    // %ci parses case-insensitive string
    let private isPremiumKeyword = %ci "PREMIUM"
    
    let private isKeyword = %ci "is"
    
    // %[x, y, ...] attempts to apply each parser until any of them succeeds
    let private boolean = %[
        // >>% discards parsed values and returns it's right hand argument (Is true in that case)
        %ci "true" >>% Is true
        %ci "false" >>% Is false
    ]
    
    // %% starts a pipe - it is a way of chaining parsers together
    // -- combines parsers
    // +. marks that value parsed by the following parser is preserved as result of the pipe
    let isPremiumCondition : Parser<IsPremiumCondition, unit> =
        // spaces succeeds on one or more whitespace characters
        %% isPremiumKeyword -- spaces1 -- isKeyword -- spaces1 -- +. boolean
        -%> auto // since we don't need to transform the output, -%> auto would do