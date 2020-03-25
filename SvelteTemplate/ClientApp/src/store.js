import { writable, readable } from "svelte/store";

export const route = writable(null);


export const reglament = readable({}, set => {
  fetch(`/api/user/reglament`).then(q => q.json().then(r => set(r)));
  return x => {};
});


export const user = readable({}, set => {
  fetch(`/api/user`).then(q => q.json().then(r => set(r)));
  return x => {};
});


function createToasts() {
  const { subscribe, set, update } = writable(null);
  let id = 0;
  let send = m => set({type:0, ...m, id: id++})
	return {
		subscribe,
    send: send,
    erorr: (str) => send({message: str, type: 1}),
    message: (str) => send({message: str, type: 0}),
    warning: (str) => send({message: str, type: 2}),
    success: (str) => send({message: str, type: 3}),
	};
}
export const toAsts = createToasts();