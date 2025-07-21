import React, { useEffect, useState } from "react";
import api from "../api/axios";

function TestApi() {
  const [data, setData] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    api
      .get("/kiraliklar")
      .then((res) => setData(res.data))
      .catch((err) => {
        console.error("API Hatası:", err);
        setError(err.message);
      });
  }, []);

  return (
    <div>
      <h2>API Testi</h2>
      {error && <p style={{ color: "red" }}>HATA: {error}</p>}
      {data && <pre>{JSON.stringify(data, null, 2)}</pre>}
    </div>
  );
}

export default TestApi;
