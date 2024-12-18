import React from "react";
import { createRoot } from "react-dom/client";
import { Provider } from "react-redux";
import App from "./App";
import { store } from "./app/store";
import "./index.css";
import { worker } from './api/server';

// Wrap app rendering so we can wait for the mock API to initialize
async function start() {

  ///////////////////////////////////////////////////////////////////////////////////////////////
  // Start our mock API server
  await worker.start({ 
    onUnhandledRequest: 'bypass' , 
    serviceWorker: {
    // This is useful if your application follows
    // a strict directory structure.
    url: './mockServiceWorker.js',
  }});

  //store.dispatch(extendedApiSlice.endpoints.getUsers.initiate())
///////////////////////////////////////////////////////////////////////////////////////////////

  const container = document.getElementById("root");

  if (container) {
    const root = createRoot(container);

    root.render(
      <React.StrictMode>
        <Provider store={store}>
          <App />
        </Provider>
      </React.StrictMode>,
    )
  } else {
    throw new Error(
      "Root element with ID 'root' was not found in the document. Ensure there is a corresponding HTML element with the ID 'root' in your HTML file.",
    )
  }
}

start();

