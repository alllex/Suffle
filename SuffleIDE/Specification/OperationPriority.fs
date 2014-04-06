﻿module Specification.OperationPriority

open Specification.Types

let binaryOps = 
    [BAdd; BSub; BDiv; BMul;
     BAnd; BOr;
     BEQ; BNEQ; BGT; BLT; BNGT; BNLT]

(*
let binaryOps = 
    [BAdd]
*)

let priority binOp =
    match binOp with
    // Arithmetics
    | BAdd | BSub -> 6
    | BDiv | BMul -> 7
    // Logic
    | BAnd -> 4
    | BOr  -> 3
    // Comparation
    | BEQ | BNEQ | BGT | BLT | BNGT | BNLT -> 5