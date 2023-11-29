import requests

response = requests.get("http://localhost:6060/api/Authorize/Login?username=testuser2&password=a123456")
print(response.text)