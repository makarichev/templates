<script>
  import { onMount } from "svelte";
  import TextField from "../../Shared/Forms/TextField.svelte";
  import DateField from "../../Shared/Forms/DateField.svelte";
  import { asLoader, validate } from "../../Shared/Actions.js";

  export let mol;
  export let save;
  export let loading;
  export let filtered = true;

  let audiences = [];

  let loadAudiences = async x => {
      loading = true;
    try {
      let r = await fetch(`/api/mols/${mol.MOL_ID}/audiences`);
      audiences = await r.json();
    } catch (error) {
      alert(error);
    }
    loading = false;
  };

  $: count = audiences.filter(x => x.IS_CHECKED).length;

  let saveLocal = async x => {
    await save(mol);

    try {
      let data = audiences
        .filter(x => x.IS_CHECKED)
        .map(x => {
          return { MOL_ID: mol.MOL_ID, AUDIENCE_ID: x.AUDIENCE_ID };
        });
      console.log(data);
      let q = await fetch(`/api/mols/${mol.MOL_ID}/audiences`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
      });

      if (q.ok) await loadAudiences();
    } catch (error) {
      alert(error);
    }
  };

  let loginfree = null;
  
  let testLogin = async x => {
    try {
      let q = await fetch(
        `/api/mols/${mol.MOL_ID}/verify?login=${x}`
      );
      loginfree = await q.json();
    } catch (error) {
      alert(error);
    }
  };

  let timer;
  $: {
      clearTimeout(timer);
      timer = setTimeout(x => testLogin(mol.NAME_LOGON), 500);
  }

  $: passRequired = !(mol.NAME_LOGON === null || mol.NAME_LOGON === '') && (mol.SQL_PASSWORD === null || mol.SQL_PASSWORD === '')

  


  let changePassword = async x => {
      try {
          let q = await fetch(`/api/mols/password?password=${mol.SQL_PASSWORD}`)
          mol.SQL_PASSWORD = await q.text()
      } catch (error) {
          alert(error)
      }

  }

  onMount(loadAudiences);
</script>

<style>
  .audience-label {
    line-height: 12px;
    color: gray;
    opacity: 0.6;
  }
  .audience-label.active {
    color: black;
    opacity: 1;
  }
</style>

<form on:submit|preventDefault={saveLocal}>

  <div class="form-row">
    <div class="form-group col-md-3">
      <label>Login:</label>
      <input class="form-control" type="search" bind:value={mol.NAME_LOGON} use:validate={!loginfree} message="Логин занят" />

      {#if loginfree}
        <small class="text-success">
          <i class="fa fa-check" />
          свободен
        </small>
      {:else}
        <small class="text-danger">
          <i class="fa fa-remove" />
          занят
        </small>
      {/if}

    </div>
    <div class="form-group col-md-6">
      <label>Пароль:</label>
      <input type="search" class="form-control" bind:value={mol.SQL_PASSWORD} 
        use:validate={passRequired} 
        message="Введите пароль" 
        on:change={changePassword}>
    </div>
    <div class="form-group col-md-3 disabled">
      <DateField text="Истекает" bind:value={mol.D_PWD_EXPIRED} />
    </div>
  </div>

  <div class="form-group">
    <label>
      <button
        on:click={x => (filtered = !filtered)}
        type="button"
        class="btn btn-link btn-sm"
        use:asLoader={loading}
        >
        Права доступа ({count})
      </button>

    </label>

    <div class="card">
      <div class="card-body">
        <div class="row">

          {#each audiences.filter(x => !filtered || x.IS_CHECKED) as item}
            <div class="form-check col-md-3">
              <input
                class="form-check-input"
                type="checkbox"
                bind:checked={item.IS_CHECKED}
                id="check{item.AUDIENCE_ID}" />
              <label
                class="form-check-label audience-label"
                for="check{item.AUDIENCE_ID}"
                class:active={item.IS_CHECKED}>
                <small>{item.DESCRIPTION}</small>
              </label>
            </div>
          {/each}
        </div>
      </div>
    </div>

  </div>

  <button use:asLoader={loading} type="submit" class="btn btn-primary">
    Сохранить
  </button>

</form>
