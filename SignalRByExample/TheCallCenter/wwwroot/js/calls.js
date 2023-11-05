// calls.js
"use strict";

$(document).ready(() => {

  let $theWarning = $("#theWarning");
  let $logBody = $("#logBody");
  let calls = [];

  $theWarning.hide();
  $logBody.on("click", ".delete-button", function () {
    deleteCall(this);
  });
  $logBody.on("click", ".resolve-button", function () {
    resolveCall(this);
  });


  const client = new signalR.HubConnectionBuilder().withUrl("/callcenter").build();
  client.on("NewCallReceived", newCall => addCall(newCall) );
  client.on("DeleteCallEvent", id => deleteCallById(id));
  client.on("ResolveCallEvent", id => resolveCallById(id));


  function addCalls() {
    $logBody.empty();
    $.each(calls, (i,c) => addCall(c));
  }

  function addCall(call) {
    let template = `<tr>
  <td>${call.name}</td>
  <td>${call.email}</td>
  <td>${moment(call.callTime).format("llll")}</td>
  <td><button class="btn btn-sm btn-warning delete-button" disabled data-id="${call.id}">Delete</button></td>
  <td><button class="btn btn-sm resolve-button" data-id="${call.id}">Set Resolved</button></td>
</tr>`;
    $logBody.append($(template));
  }

  function deleteCall(button) {
    let id = $(button).attr("data-id");
    $.ajax({
      url: `/api/calls/${id}`,
      method: "delete"
    })
      .then(res => {
        $(button).closest("tr").remove();
      });
  }

  function resolveCall(button) {
    let id = $(button).attr("data-id");
    $.ajax({
      url: `/api/calls/resolve/${id}`,
      method: "POST"
    })
      .then(res => {
        $(button).attr("disabled", false);
      });
  }

  function deleteCallById(id) {
    const button = $(".delete-button").filter(function () { return $(this).data("id") == id; })[0];
    button.closest("tr").remove();
  }

  function resolveCallById(id) {
    const button = $(".delete-button").filter(function () { return $(this).data("id") == id; })[0];
    $(button).attr("disabled", false);

  }

  function getCalls() {
    $.getJSON("/api/calls")
      .then(res => {
        calls = res;
        addCalls();
        //client.start();
        client.start().then(() => client.invoke("JoinCallCenters"));
      })
      .catch(() => {
        $theWarning.text("Failed to get calls...");
        $theWarning.show();
      });
  }

  getCalls();
});