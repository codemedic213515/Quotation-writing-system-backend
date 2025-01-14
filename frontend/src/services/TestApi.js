import React, { useEffect, useState } from "react";
import axios from "axios";

const TestAPI = () => {
  const [message, setMessage] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("http://localhost:5095/api/auth/test"); // Correct GET request
        setMessage(response.data);
      } catch (error) {
        console.error("Error fetching data:", error.message);
        setMessage(`Failed to connect: ${error.message}`);
      }
    };

    fetchData();
  }, []);

  return <div>{message}</div>;
};

export default TestAPI;
