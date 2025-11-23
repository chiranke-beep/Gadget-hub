// Utility for quotation API calls


// Send quotation request to all 3 distributors in parallel
export async function requestQuotation(productRequests) {
  const endpoints = [
    'http://localhost:5258/api/quotations', // ElectroComApi
    'http://localhost:5213/api/quotations', // TechWorldApi
    'http://localhost:5019/api/quotations'  // GadgetCentralApi
  ];
  
  // Send the productRequests array directly (QuotationsController expects List<ProductRequestDto>)
  const fetches = endpoints.map(async (url) => {
    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(productRequests)
      });
      if (!response.ok) {
        // Return error info for this distributor
        const errorText = await response.text();
        console.error(`Error from ${url}:`, errorText);
        return { url, error: true, status: response.status, message: errorText };
      }
      return { url, error: false, data: await response.json() };
    } catch (error) {
      console.error(`Network error from ${url}:`, error);
      return { url, error: true, status: 0, message: error.message };
    }
  });
  return Promise.all(fetches);
}
