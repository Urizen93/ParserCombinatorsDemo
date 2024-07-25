namespace ParserCombinators.Model.Tests

open ParserCombinators.Model
open Xunit

type ``AgeCondition parser tests`` (output) =
    let shouldBe = shouldBe output
    let shouldFail = shouldFail output
    let parser = AgeConditionParser.ageCondition
    
    [<Theory
    InlineData "age = 40"
    InlineData "AGE>= -18">]
    member _.``Should fail on malformed conditions`` input =
        parser
        |> parse input
        |> shouldFail
    
    [<Theory; ClassData(typeof<AgeConditions>)>]
    member _.``Should parse conditions`` input expected =
        parser
        |> parse input
        |> shouldBe expected

and AgeConditions () as this =
    inherit TheoryData<string, AgeCondition>()
    do
        this.Add("age > 40", AgeCondition.Greater <| 40)
        this.Add("AGE>=18", AgeCondition.GreaterOrEqual <| 18)
        this.Add("Age  <   85", AgeCondition.Less <| 85)
        this.Add("Age <= 14", AgeCondition.LessOrEqual <| 14)