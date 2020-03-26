import { writable, readable } from "svelte/store";

export const route = writable(null);


export const reglament = readable({}, set => {
  fetch(`/api/user/reglament`).then(q => q.json().then(r => set(r)));
  return x => { };
});


export const user = readable({}, set => {
  fetch(`/api/user`).then(q => q.json().then(r => set(r)));
  return x => { };
});


function createToasts() {
  const { subscribe, set, update } = writable(null);
  let id = 0;
  let send = m => set({ type: 0, delay: 5000, autohide: true, ...m, date: new Date() })
  return {
    subscribe,
    send: send,
    'error': (str) => send({ message: str, type: 1 }),
    message: (str) => send({ message: str, type: 0 }),
    warning: (str) => send({ message: str, type: 2 }),
    success: (str) => send({ message: str, type: 3 }),
  };
}
export const toAsts = createToasts();




function createSocket() {

  let socket, sessionId
  const { subscribe } = readable(null, set => {

    socket = new WebSocket(`ws://${window.location.host}/api/mols/connect`)
    socket.onclose = function (x) { console.log('CLOSE', x) }
    socket.onopen = x => {
      console.log(x)
      // let json = JSON.stringify(x.data);
      // sessionId = json.id;
    }
    socket.onmessage = x => {
      console.log(x)
      set(x);
    }
    socket.onerror = x => console.error('ERROR', x.message)

    return function stop() {
      socket.close(1000, "работа закончена");
    }

  });

  return { subscribe, send: (x) => socket.send(x) };
}


export const socket = createSocket();
