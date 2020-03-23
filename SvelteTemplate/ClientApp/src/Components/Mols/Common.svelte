<script>
  import { onMount, onDestroy, createEventDispatcher, getContext } from "svelte";
  import { reglament } from "../../store.js";
  import {asDate, asLoader} from "../../Shared/Actions.js";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import DateField from "../../Shared/Forms/DateField.svelte";
  import WeekField from "../../Shared/Forms/WeekField.svelte";
  import PeriodField from "../../Shared/Forms/PeriodField.svelte";
  import PickerField from "../../Shared/Forms/PickerField.svelte";

  let dispatcher = createEventDispatcher();
  export let mol = {};

  
  export let save = null;

  export let depts = [];
  export let posts = [];
  export let loading;


  onDestroy(x => {
    //if (confirm("Есть изменения")) save(mol);
  })

  

</script>

<form on:submit|preventDefault={x => save(mol)} on:reset={e => alert()} id="commonForm">


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
  <div class="form-group">
    <TextField text="Адрес" required bind:value={mol.ADDRESS} />
  </div>
  <div class="form-group">
    <TextField text="Паспортные данные" bind:value={mol.PASSPORT} />
  </div>
  <div class="form-row">
    <div class="form-group col-md-5">
      <PickerField text="Отдел" list={depts} bind:value={mol.DDEPT_ID} required/>
    </div>
    <div class="form-group col-md-4">
      <PickerField text="Должность" list={posts} bind:value={mol.POST_ID}/>
    </div>
    <div class="form-group col-md-3">
        <DateField text="Д.р" bind:value={mol.BIRTHDAY}></DateField>
    </div>
  </div>

  

  <button
    use:asLoader={loading}
    type="submit"
    class:disabled={!$reglament.allowEdit}
    class="btn btn-primary">
    Сохранить
  </button>
  
  


</form>


