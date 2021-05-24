import React, { useState, useEffect } from 'react';
import axios from 'axios';
import JokeCard from '../Components/JokeCard';


const ViewAll = () => {
    const [jokes, setJokes] = useState([]);

    useEffect(() => {
        const getJokes = async () => {
            const { data } = await axios.get('/api/joke/getalljokes');
            setJokes(data);
        }
        getJokes();
    }, []);

    return (
        <div className="row">
            <div className="col-md-6 offset-md-3">
            {jokes.map(j => <JokeCard key={j.id}
                            joke={j}     
                        />)}
            </div>
        </div>
    );

}

export default ViewAll;