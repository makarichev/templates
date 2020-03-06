<script>
  import { Modal, ModalBody, ModalHeader, ModalFooter } from "sveltestrap";
  export let show = false;
  export let apply = () => {};

</script>
<Modal isOpen={show} toggle={x => (show = !show)}>

<form on:submit|preventDefault={x => {apply(); show = false;}}>

  <ModalHeader toggle={x => (show = !show)}>
    Фильтр
  </ModalHeader>
  <ModalBody>
    <div>
        <slot>Фильтр неопределен</slot>
    </div>
  </ModalBody>
  <ModalFooter>
    <button class="btn btn-outline-primary" type="submit">Применить</button>
    <button class="btn btn-outline-secondary" type="button" on:click={(x => show = false)}>Закрыть</button>
  </ModalFooter>

</form>
</Modal>
