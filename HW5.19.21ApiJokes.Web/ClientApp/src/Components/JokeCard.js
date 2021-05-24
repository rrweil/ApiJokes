import React, { useEffect} from 'react';

const JokeCard = ({joke}) => {
    const {setup, punchline, likes} = joke;

    return (
        
                <div className="card card-body bg-light mb-3">
                    <h5>{setup}</h5>
                    <h5>{punchline}</h5>
                    <span>Likes: {likes.filter( l => l.liked ).length}</span>
                    <span>Dislikes: {likes.filter( l => !l.liked ).length}</span>
                </div>
    )
}
export default JokeCard;