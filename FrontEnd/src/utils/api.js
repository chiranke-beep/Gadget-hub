// Utility for API calls

export async function fetchProducts() {
  const response = await fetch('/api/product');
  if (!response.ok) {
    throw new Error('API error: ' + response.status);
  }
  return response.json();
}

export async function fetchProductById(id) {
  const response = await fetch(`/api/product/${id}`);
  if (!response.ok) {
    throw new Error('API error: ' + response.status);
  }
  return response.json();
}

export async function fetchUsers() {
  const response = await fetch('/api/user');
  if (!response.ok) {
    throw new Error('API error: ' + response.status);
  }
  return response.json();
}

export async function createUser(userData) {
  const response = await fetch('/api/user', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(userData)
  });
  if (!response.ok) {
    throw new Error('API error: ' + response.status);
  }
  return response.json();
}

export async function updateUser(id, userData) {
  const response = await fetch(`/api/user/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(userData)
  });
  if (!response.ok) {
    throw new Error('API error: ' + response.status);
  }
  // If response is 204 No Content, don't try to parse JSON
  if (response.status === 204) {
    return null;
  }
  // Only parse JSON if there is content
  const text = await response.text();
  return text ? JSON.parse(text) : null;
}

// Request quotations through GadgetHubApi (which will forward to all distributors)
export async function requestQuotationsFromAllDistributors(productRequests) {
  try {
    const response = await fetch('http://localhost:5254/api/Quotation/request', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        userId: 1, // Default user ID
        items: productRequests.map(request => ({
          gadgetHubId: request.productId,
          quantity: request.quantity
        }))
      })
    });
    
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`GadgetHub API error: ${response.status} - ${errorText}`);
    }
    
    const result = await response.json();
    console.log('GadgetHub API response:', result);
    return result;
  } catch (error) {
    console.error('Error requesting quotations:', error);
    throw error;
  }
}

// Store distributor response in FinalResponses
export async function storeDistributorResponse(responseData) {
  try {
    const response = await fetch('http://localhost:5254/api/Quotation/store-response', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(responseData)
    });
    
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Store response error: ${response.status} - ${errorText}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error storing distributor response:', error);
    throw error;
  }
}

// Compare quotations and get the best one
export async function compareQuotations(quotationRequestId) {
  try {
    const response = await fetch(`http://localhost:5254/api/Quotation/compare/${quotationRequestId}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' }
    });
    
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Compare quotations error: ${response.status} - ${errorText}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error comparing quotations:', error);
    throw error;
  }
}

// Process complete quotation workflow
export async function processQuotation(quotationRequestId, distributorResponses) {
  try {
    const response = await fetch('http://localhost:5254/api/Quotation/process-quotation', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        quotationRequestId: quotationRequestId,
        distributorResponses: distributorResponses
      })
    });
    
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Process quotation error: ${response.status} - ${errorText}`);
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error processing quotation:', error);
    throw error;
  }
}
