module Info.View

open Fable.Helpers.React
open Fable.Helpers.React.Props

let root =
  div
    [ ClassName "content" ]
    [ h1 [] [ str "Motivation" ]
      p[]
        [
          strong[][str "Hiring is broken."]
          str "  Although the problems with hiring in the Software Industry cannot all be solved though a job website, Sieve "
          str "aims to improve the interviewing experienceby giving Software Developers as much information"
          str " as possible throughout the hiring process, and allowing maximum control over how their information is used."
        ]
      p []
        [ str "Other job websites are designed with"
          strong [ ][str " recruiters and hiring managers "]
          str "in mind."
        ]
      p []
        [
          str "But everyone knows that it's a"
          strong [] [str " seller's market "]
          str "for Software Development labor."
        ]
      p []
        [
          str "That's why Sieve's goal is to be the only job website that is"
          strong [] [str " single-mindedly "]
          str "focused on providing the best experience for Software Developers."
        ]
      h1 [] [ str "Goals" ]
      p []
        [
          str "We have set out to make Sieve the best job site for Software Developers with the following goals in mind:"
        ]
      ul []
        [
          li []
            [
            p []
              [
                strong [][str "Developer Empowerment "]
                str "- Use structured data to control only the jobs you want to see, whether searching on your own, subscribing to periodic email summaries, or receiving emails from job recruiters."
              ]
            ]
          li []
            [
              p []
                [
                  strong[][str "Job Searching Experience "]
                  str "- Track recruiter quality metrics to minimize ghosting and needless mass emailing"
                ]
            ]
          li []
            [
              p []
                [
                  strong[][str "Candidate hiring experience "]
                  str "- Just because of Sieve's single-minded focus on creating the best experience for Software Developers doesn't mean that "
                  str "we don't see some obvious ways of improving the experience for Recruiters and Hiring Managers also, without compromising the experience"
                  str " of our core customers.  "
                  em [] [str "Straightforward, up-front pricing " ]
                  str "is one promise that Sieve will always fulfill.  No more \"medical insurance\"-style pricing."
                ]
            ]
        ]
    ]
