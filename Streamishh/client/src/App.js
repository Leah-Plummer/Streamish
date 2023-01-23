import React from "react";
import "./App.css";
import VideoList from "./components/VideoList";
import  VideoForm  from "./components/VideoForm";
//import VideoSearch from "./components/SearchVideos";


function App() {
  return (
    <div className="App">
      <VideoForm />
      {/* <VideoSearch/> */}
      <VideoList />
    </div>
  );
}

export default App;