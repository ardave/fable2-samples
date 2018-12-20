module Global

type Page =
  | Home
  | About
  | JobCreate
  | JobView of string
  | JobSearch
  | Confirmation
  | SignIn
  | SignUp
  | VerifyAccount of string

let toHash page =
  match page with
  | About           -> "#about"
  | Home            -> "#home"
  | JobCreate       -> "#jobcreate"
  | JobView _       -> "#jobview"
  | JobSearch       -> "#jobsearch"
  | Confirmation    -> "#confirmation"
  | SignIn          -> "#signin"
  | SignUp          -> "#signup"
  | VerifyAccount _ -> "#verifyaccount"
