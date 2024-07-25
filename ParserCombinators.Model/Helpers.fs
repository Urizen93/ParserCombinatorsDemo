namespace ParserCombinators.Model

module Helpers =

    let (<!>) = Option.map
    
    let (<|>%) option resolution = Option.defaultValue resolution option
    
    let curry f = fun x -> fun y -> f (x, y)