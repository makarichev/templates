<script>
  import { onMount, createEventDispatcher, getContext } from "svelte";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import PickerField from "../../Shared/Forms/PickerField.svelte";

  let dispatcher = createEventDispatcher();
  export let mol = null;

  export let depts = [];
  export let posts = [];
  export let loading = false;


  export let save = null;


</script>


<form on:submit|preventDefault={save(mol)}>
  <div class="form-row">
    <div class="form-group col-md-4">
      <TextField text="Фамилия" required bind:value={mol.SURNAME} />
    </div>
    <div class="form-group col-md-4">
      <TextField text="Имя" required bind:value={mol.NAME1} />
    </div>
    <div class="form-group col-md-4">
      <TextField text="Отчество" required bind:value={mol.NAME2} />
    </div>
  </div>

    <div class="form-row">
    <div class="form-group col-md-3">
      <TextField text="Телефон" bind:value={mol.PHONE} />
    </div>
    <div class="form-group col-md-3">
      <TextField text="Внутр." bind:value={mol.PHONE_LOCAL} />
    </div>
    <div class="form-group col-md-3">
      <TextField text="Email" bind:value={mol.EMAIL} />
    </div>
    <div class="form-group col-md-3">
      <TextField text="Комната" bind:value={mol.ROOM} />
    </div>
  </div>


  <button type="submit" class="btn btn-primary">Сохранить</button>

</form>