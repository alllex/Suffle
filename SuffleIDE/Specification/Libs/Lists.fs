module Suffle.Specification.Libs.Lists

let typeName = "List"
let consName = "Cons"
let emptName = "Nil"

let lib = """

datatype List 'a =
| Cons 'a (List 'a)
| Nil
end

datatype Pair 'a 'b = 
| P 'a 'b
end

fun :: (List 'a) -> 'a
head xs =
    case xs of
    | x : _ -> x
    end

fun :: (List 'a) -> (List 'a)
tail xs = 
    case xs of
    | _ : tl -> tl
    end

fun :: (List 'a) -> int
length list = 
    case list of
    | [] -> 0
    | _ : rest -> 1 + length rest
    end

fun :: (List 'a) -> (List 'a)
rev xs =
    let 
        fun :: (List 'a) -> (List 'a) -> (List 'a)
        rev' xs rest = 
            case rest of
            | [] -> xs
            | x : rs -> rev' (x : xs) rs
            end
    in
        rev' [] xs
    end
    
fun :: ('a -> 'b) -> (List 'a) -> (List 'b)               
map f xs =
    case xs of
    | [] -> []
    | x : xs' -> f x : (map f xs')
    end

fun :: ('a -> bool) -> (List 'a) -> (List 'a)
filter f xs =
    case xs of
    | [] -> []
    | x : xs' -> 
        let 
            val :: (List 'a)
            rest = filter f xs'
        in 
            if f x then x : rest else rest end
        end 
    end

fun :: ('a -> 'b -> 'a) -> 'a -> (List 'b) -> 'a
foldl f acc xs =
    case xs of
    | [] -> acc
    | x : xs' -> foldl f (f acc x) xs'
    end

fun :: ('b -> 'a -> 'a) -> (List 'b) -> 'a -> 'a
foldr f xs acc =
    case xs of
    | [] -> acc
    | x : xs' -> f x (foldr f xs' acc)
    end   

fun :: List 'a -> List 'a -> List 'a
append xs ys = 
    case xs of
    | [] -> ys
    | x:xs -> x : append xs ys
    end

fun :: List (List 'a) -> List 'a
concat xxs =
    case xxs of
    | [] -> []
    | xs:xxs -> append xs (concat xxs)
    end

fun :: ('a -> bool) -> List 'a -> bool 
exists p xs =
    case xs of
    | [] -> false
    | x:xs -> if p x then true else exists p xs end
    end

fun :: List 'a -> 'a -> List 'a
push xs x' =
    case xs of
    | [] -> [x']
    | x:xs -> x : push xs x'  
    end  

fun :: List 'a -> List 'a
qsort xs =
    case xs of
    | [] -> [] | x:xs -> concat [qsort (filter (\y -> y < x) xs), [x], qsort (filter (\y -> y >= x) xs)]
    end

fun :: ('a -> 'a -> bool) -> List 'a -> List 'a
mergesort pred xs =
    let 

        fun :: List 'a -> Pair (List 'a) (List 'a)
        split xs =
            case xs of
            | x:y:zs -> case (split zs) of | P xs ys -> P (x:xs) (y:ys) end
            | _ -> P xs []
            end

        fun :: ('a -> 'a -> bool) -> List 'a -> List 'a -> List 'a
        merge pred xs ys =
            case (P xs ys) of
            | P xs [] -> xs
            | P [] ys -> ys
            | P (x:xs) (y:ys) -> if (pred x y) then (x : (merge pred xs (y:ys))) else (y : (merge pred (x:xs) ys)) end
            end

    in

        case xs of
        | [] -> [] | [x] -> [x]
        | _ -> case (split xs) of | P xs1 xs2 -> merge pred (mergesort pred xs1) (mergesort pred xs2) end
        end

    end         
   
"""