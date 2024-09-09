import { FC } from "react"

interface PostProp{
    message: string,
    name: string,
    dateOfPublich: string
}

const Post: FC<PostProp> = ({message, name, dateOfPublich}) =>
{
    return (
        <div className="post">
            The text "{message}" published by user {name} on {dateOfPublich}.
        </div>
    )
}

export default Post