import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import { addVideo, getAllVideos} from "../modules/videoManager";

export const VideoSearch = ({ setterFunction}) => {

   
    return (
        <div>
            <input 
                onChange={
                    (changeEvent) => {
                    //change the state in the parent component (ProductContainer)
                       setterFunction(changeEvent.target.value) 
                    }
    
                }
            type="text" id="searchField" placeholder="Video Title" />
            {/* <button onClick={() => { setDescending(true) }}>Results Descending?</button> */}
        </div>
        )
};

export default VideoSearch;