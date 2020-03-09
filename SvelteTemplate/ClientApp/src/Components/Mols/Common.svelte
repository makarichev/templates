<script>
  import { onMount, createEventDispatcher } from "svelte";
  import { reglament } from "../../store.js";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import DateField from "../../Shared/Forms/DateField.svelte";
  import WeekField from "../../Shared/Forms/WeekField.svelte";
  import PeriodField from "../../Shared/Forms/PeriodField.svelte";
  import PickerField from "../../Shared/Forms/PickerField.svelte";

  let dispatcher = createEventDispatcher();
  export let mol = {};

  $: valid =
    mol.SURNAME &&
    mol.NAME1 &&
    mol.NAME2 &&
    mol.ADDRESS &&
    mol.DDEPT_ID &&
    mol.POST_ID;

  function save() {
    if ($reglament.alloEdit && valid) dispatcher("save", mol);
  }

  let depts = [];
  let posts = [];

  onMount(async x => {
    let q = await fetch(`/api/mols/depts`);
    depts = await q.json();
    q = await fetch(`/api/mols/posts`);
    posts = await q.json();
  });



  let weekId = null, periodId = 201202;
</script>

<form on:submit|preventDefault={save}>
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
      <PickerField text="Должность" list={posts} bind:value={mol.POST_ID} required/>
    </div>
    <div class="form-group col-md-3">
        <DateField text="Д.р" bind:value={mol.BIRTHDAY} required></DateField>
    </div>
  </div>
  
  <button
    type="submit"
    class:disabled={!$reglament.allowEdit || !valid}
    class="btn btn-primary">
    Сохранить
  </button>
</form>
