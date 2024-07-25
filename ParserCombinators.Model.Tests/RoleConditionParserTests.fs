namespace ParserCombinators.Model.Tests

open ParserCombinators.Model
open Xunit

type ``RoleCondition parser tests`` (output) =
    let shouldBe = shouldBe output
    let shouldFail = shouldFail output
    let parser = RoleConditionParser.roleCondition
    
    [<Theory
    InlineData "role = 'nonsense'"
    InlineData "role in('Customer')"
    InlineData "role not in 'Manager'">]
    member _.``Should fail on malformed conditions`` input =
        parser
        |> parse input
        |> shouldFail
    
    [<Theory; ClassData(typeof<RoleConditions>)>]
    member _.``Should parse conditions`` input expected =
        parser
        |> parse input
        |> shouldBe expected

and RoleConditions () as this =
    inherit TheoryData<string, RoleCondition>()
    do
        this.Add("role='Customer'", RoleCondition.Equals <| "Customer")
        this.Add("role != 'Admin'", RoleCondition.NotEquals <| "Admin")
        this.Add("ROLE in ('Manager','Admin')", RoleCondition.In <| Set.ofList ["Manager"; "Admin"])
        this.Add("RolE not IN ('Customer','Manager')", RoleCondition.NotIn <| Set.ofList ["Customer"; "Manager"])