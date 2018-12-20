module User

type UserType =
| JobSeeker
| Recruiter

type PasswordHash = PasswordHash of string

type User = {
    firstName    : string
    lastName     : string
    userType     : UserType
    passwordHash : PasswordHash
}