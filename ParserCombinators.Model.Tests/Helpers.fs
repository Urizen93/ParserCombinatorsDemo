[<AutoOpen>]
module Helpers
    
open FParsec
open Xunit
open Xunit.Abstractions

let parse source parser =
    match runParserOnString parser () "" source with
    | Success (result, _, _) -> Choice1Of2 result
    | Failure (error, _, _) -> Choice2Of2 error

let shouldBe (output : ITestOutputHelper) (expected : 'a) (actualChoice : Choice<'a, string>) =
    match actualChoice with
    | Choice1Of2 actual ->
        output.WriteLine $"%A{actual}"
        Assert.Equal<'a> (expected, actual)
    | Choice2Of2 error -> Assert.Fail error

let shouldFail (output : ITestOutputHelper) (actualChoice : Choice<'a, string>) =
    match actualChoice with
    | Choice1Of2 actual -> Assert.Fail $"%A{actual}"
    | Choice2Of2 error -> output.WriteLine error

let (<!>) = Set.map