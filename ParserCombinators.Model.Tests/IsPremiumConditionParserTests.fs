namespace ParserCombinators.Model.Tests

open ParserCombinators.Model
open Xunit

type ``IsPremium parser tests`` (output) =
    let shouldBe = shouldBe output
    let shouldFail = shouldFail output
    let parser = IsPremiumConditionParser.isPremiumCondition
    
    [<Theory
    InlineData "Premium = true"
    InlineData "Premium is unknown">]
    member _.``Should fail on malformed conditions`` input =
        parser
        |> parse input
        |> shouldFail
    
    [<Theory; ClassData(typeof<IsPremiumConditions>)>]
    member _.``Should parse conditions`` input expected =
        parser
        |> parse input
        |> shouldBe expected

and IsPremiumConditions () as this =
    inherit TheoryData<string, IsPremiumCondition>()
    do
        this.Add("premium is true", IsPremiumCondition.Is true)
        this.Add("PREMIUM IS FALSE", IsPremiumCondition.Is false)